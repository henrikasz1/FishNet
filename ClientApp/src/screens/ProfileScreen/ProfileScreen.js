import { View, Text, ScrollView, StyleSheet, Image, ActivityIndicator, TouchableWithoutFeedback, useWindowDimensions } from 'react-native'
import React, { useEffect, useState} from 'react'
import Header from '../../components/Header';
import Footer from '../../components/Footer';
import axios from 'axios';
import { BaseUrl } from '../../components/Common/BaseUrl';
import DeadFish from '../../../assets/images/deadFish.png';
import DefaultUserPhoto from '../../../assets/images/default-user-image.png'
import { useNavigation } from '@react-navigation/native';
import ProfileHeader from '../../components/ProfileHeader';
import Photo from '../../components/Photo';
import Block from '../../components/Feed/FeedBlock';

const ProfileScreen = ({ route }) => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);
  const [loading, setLoading] = useState(true);
  const getUserById = `${BaseUrl}/api/user/getbyid/${route.params.userId}`;
  const getCurrentUserId = `${BaseUrl}/api/user/getuserid`;
  const getUserPhotos = `${BaseUrl}/api/userphoto/getall/${route.params.userId}`;
  const getFriendshipStatus = `${BaseUrl}/api/friends/getfriendshipstatus/${route.params.userId}`;
  const [results, setResults] = useState({});
  const [currentUserId, setCurrentUserId] = useState("");
  const [photos, setPhotos] = useState(undefined);
  const [friends, setFriends] = useState(false);
  const [posts, setPosts] = useState(undefined);
  const [batchNumber, setBatchNumber] = useState(0);
  const {height} = useWindowDimensions();

  const onPressHome = () => {
    navigation.navigate("MainScreen")
  }

  const onPressProfile = () => {
      if (route.params.pop == undefined && currentUserId !== route.params.userId)
      {
        navigation.push("ProfileScreen", {currentBackScreen: route.params.currentBackScreen, backScreen: route.params.backScreen, userId: currentUserId});
      }
  }

  const onPressShop = () => {
      navigation.navigate("ShopScreen")
  }

  const onPressEvent = () => {
      navigation.navigate("EventScreen")
  }

  const onPressGroup = () => {
      navigation.navigate("GroupScreen")
  }

  const onPressSearch = () => {
    navigation.navigate("SearchScreen", {backScreen: "ProfileScreen"})
  }

  const formatName = () => {
    var name = results.name + " " + results.lastName;

    if (name.length > 23)
    {
      name = name.substring(0, 20) + "..."
    }

    return name;
  }

  const pickFooterProfileButtonColor = () => {
    if (route.params.userId == currentUserId)
    {
      return '#3B71F3'
    }
  }

  const getPosts = async () => {
    const getUserPosts = `${BaseUrl}/api/post/userposts/${route.params.userId}?batchSize=${batchNumber}`;
    await axios
          .get(getUserPosts)
          .then(response => {
            if (batchNumber === 0)
            {
              setPosts(response.data)
            }
            else if (response.data.length !== 0)
            {
              setPosts([...posts, ...response.data]);
            }
          })
          .catch(err => {
            console.log(err);
          })
          
    await setLoading(loading => (false));
  }

  const isCloseToBottom = ({ layoutMeasurement, contentOffset, contentSize }) => {
    const paddingToBottom = 20;
    return layoutMeasurement.height + contentOffset.y >=
      contentSize.height - paddingToBottom;
  };

  const handleLoadMore = async () => {
    await setBatchNumber(batchNumber + 1);
    await getPosts();
  }

  useEffect(() => {

    const getData = async () => {
      await axios
        .get(getCurrentUserId)
        .then(response => {
          setCurrentUserId(response.data);
        })
        .catch(err => {
          console.log(err)
        })

      await axios
        .get(getUserById)
        .then(response => {
          setResults(response.data)
        })
        .catch(err => {
          console.log(err)
        })

      if (loading)
      {
        await axios
          .get(getFriendshipStatus)
          .then(response => {
            setFriends(response.data);
          })
          .catch(err => {
            console.log(err);
          })
        
        await axios
          .get(getUserPhotos)
          .then(response => {
            setPhotos(response.data)
          })
          .catch(err => {
            console.log(err)
          })

        await getPosts();
      }
    }

    getData();

  }, [loading])

  return (

    <View style={styles.container}>
      <ProfileHeader
        onPressBack={() => navigation.pop() }
        name={formatName()}
      />
      
      { loading ?
        <View style={styles.activityIndicator}>
          <ActivityIndicator size="large" color="#3B71F3" />
        </View>
        :
        <ScrollView style={styles.container} ref={scrollRef} onScroll={(event) => {
          if (isCloseToBottom(event.nativeEvent)) {
            handleLoadMore();
          }
        }}>
          <View style={styles.imageContainer}>
            {results.mainUserPhotoUrl !== "" ?
                <View>
                  <Image
                    source={{ uri: results.mainUserPhotoUrl }}
                    style={styles.image}
                  />
                </View>
                :
                <View>
                  <Image
                    source={DefaultUserPhoto}
                    style={styles.image}
                  />
                </View>
            }
            <Text style={styles.name}>{results.name + " " + results.lastName}</Text>
          </View>

          { !friends && results.isProfilePrivate ?
            <View style={styles.privateProfile}>
              <Image source={DeadFish} style={{height: height * 0.15}} resizeMode="contain" />
              <Text> We're sorry, this user account is private </Text>
            </View>
            :
            <View>
              <View style={styles.contentContainer}>
                <Text style={styles.font}> Photos </Text>
                {photos !== undefined && photos !== "" &&
                    <View style={styles.photos}>
                      {photos.map((photo, key) => (
                        <TouchableWithoutFeedback key={photo.id} onPress={() => console.warn("dfd")/*() => navigation.navigate("PhotosScreen", {firstPhotoId: photo.id})*/}>
                          <View width={"33.3%"}>
                            <Photo
                              id={photo.id}
                              photoUrl={photo.url}
                              key={key}
                            />
                          </View>
                        </TouchableWithoutFeedback>))}
                    </View>}
              </View>

              <View>
                <Text style={styles.font}> Posts </Text>

                {posts !== undefined && posts !== "" && posts
                  .map(({ title, photos, postId, userId, likesCount, body, commentsCount }, index) => (
                  <Block
                    title={title}
                    photo={photos}
                    caption={body}
                    key={index}
                    postId={postId}
                    userId={userId}
                    likesCount={likesCount}
                    likerId={currentUserId}
                    commentsCount={commentsCount}
                    isFriendPost={true}
                  />
                  ))
                }
              </View>

            </View>}

      </ScrollView>}

      <Footer
        style={styles.footer}
        profileC={pickFooterProfileButtonColor()}
        onPressHome={onPressHome}
        onPressProfile={onPressProfile}
        onPressShop={onPressShop}
        onPressEvent={onPressEvent}
        onPressGroup={onPressGroup}
      />

    </View>
  )
}

const styles = StyleSheet.create({
    container: {
      flex: 1,
      width: '100%'
    },
    imageContainer: {
      alignItems: 'center',
      borderBottomWidth: 0.6,
      borderColor: "#DCDCDC",
    },
    contentContainer: {
      flex: 1,
      paddingVertical: '3%',
      borderBottomWidth: 1,
      borderColor: "#DCDCDC",
    },
    image: {
      marginTop: '10%',
      alignItems: 'center',
      height: 150,
      width: 150,
      borderRadius: 75
    },
    name: {
      marginTop: '5%',
      fontSize: 25,
      color: 'black',
      fontWeight: 'bold',
      paddingBottom: '5%'
    },
    photos: {
      marginTop: 2,
      flexDirection: 'row',
      flexWrap: 'wrap'
    },
    activityIndicator: {
      flex: 1,
      alignItems: 'center',
      justifyContent: 'center'
    },
    privateProfile: {
      marginTop: '25%',
      alignItems: 'center'
    },
    font: {
      paddingLeft: '2%',
      fontWeight: 'bold',
      fontSize: 20,
      color: '#232b2b',
      marginVertical: '1%'
    }
})

export default ProfileScreen