import React, { useState } from 'react';
import { View, Image, StatusBar, Text, TouchableOpacity, TouchableWithoutFeedback } from 'react-native';
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import axios from 'axios';
import { useNavigation } from '@react-navigation/native';
import CaptionComponent from './CaptionComponent';
import CarouselComponent from './CarouselComponent';
import { BaseUrl } from '../Common/BaseUrl';

import DefaultUserPhoto from '../../../assets/images/default-user-image.png'

export default function Block({ title, photo, caption, userId, postId, likesCount, likerId }) {

  const [profilePicture, setProfilePicture] = useState(null);
  const [userName, setUserName] = useState('');
  const [loading, setLoading] = useState(true);
  const [hasMyLike, setHasMyLike] = useState(false);
  const [selfLikesCount, setLikesCount] = useState(likesCount);
  const navigation = useNavigation();

  const loadLikes = async () => {
    const haveLikedUrl = `${BaseUrl}/api/likes/postlikedby/${postId}`;
    return axios.get(haveLikedUrl).then(response => {
      if (response.data && Array.isArray(response.data)) {
        const likerIds = response.data.map(r => r.userId);
        setHasMyLike(likerIds.includes(likerId));
        setLikesCount(likerIds.length);
      }
    }).catch(ignore => {});
  }

  const handleLoad = () => {
    const mainPhotoUrl =  `${BaseUrl}/api/userphoto/getmainphoto/${userId}`;
    const profilePhotoPromise = axios.get(mainPhotoUrl).then(response => {
      if (response.data) {
        setProfilePicture(response.data.url);
      }
    }).catch(ignore => {});

    const userNameUrl =  `${BaseUrl}/api/user/getname/${userId}`;
    const userNamePromise = axios.get(userNameUrl).then(response => {
      if (response.data) {
        setUserName(response.data);
      }
    }).catch(ignore => {});

    return Promise.all(profilePhotoPromise, userNamePromise, loadLikes());
  }

  if (loading){
    handleLoad().then(() => setLoading(false));
  }

  const changePostLikes = async () => {
    const url = `${BaseUrl}/api/likes/${hasMyLike ? 'un' : ''}likepost/${postId}`;
    await axios.post(url);
    await loadLikes();
  }

  const goToComments = () => {
    navigation.navigate('CommentsScreen', {
      postId,
      commentWriterId: likerId
    });
  }

  const styles = {
    item: {
      color: 'black',
      backgroundColor: 'white',
      paddingVertical: 6,
      marginBottom: 5,
    },
    title: {
      color: 'black',
      fontSize: 19,
      // fontWeight: 'bold',
      paddingBottom: 4,
      paddingHorizontal: 8,
      textAlign: 'left'
    },
    cardImage: {
      width: '100%',
      aspectRatio: 1
    },
    text: {
      paddingHorizontal: '3%',
      marginBottom: '3%'
    },
    profileImage: {
      marginLeft: '5%',
      height: 35,
      width: 35,
      borderRadius: 100
    },
    firstBlock: {
      width: '85%',
    },
    secondBlock: {
      // width: '15%',
      flexDirection: 'row',
      paddingBottom: '1%'
    },
    profile: {
      paddingRight: '3%',
      paddingHorizontal: '1%',
      height: 50,
      backgroundColor: 'white',
      alignItems: 'center',
      borderBottomWidth: 0.8,
      borderColor: '#d3d3d3',
      flexDirection: 'row',

      width: '100%',
    },
    row: {
      flexDirection: 'row',
      width: '100%',
      alignItems: 'center'
    },
    icon: {
      // borderRadius: 100,
      height: 40,
      width: 40,
      alignItems: 'center',
      justifyContent: 'center'
    },
  };

  //todo make post work without photo

  return (
    <View style={styles.item}>
      <View style={styles.profile}>

        <View style={styles.secondBlock}>
          {profilePicture !== undefined ?
            <Image
              source={{ uri: profilePicture }}
              style={styles.profileImage}
            />
          :
            <Image
              source={DefaultUserPhoto}
              style={styles.profileImage}
            />
          }
        </View>
        <View style={styles.firstBlock}>
          <Text style={styles.title}>
            {userName || "State not set..."}
          </Text>
        </View>
      </View>
      {photo === undefined ? <View></View> : (
        <View style={{ width: '100%' }}>
          <CarouselComponent pics={photo} style={styles.cardImage}/>
        </View>
      )}
      <View styles={styles.row}>
        <TouchableWithoutFeedback onPress={changePostLikes}>
          <View style={styles.icon}>
            <Icon name="heart" size={23} color={hasMyLike ? "crimson" : "black"} />
            <Text>{selfLikesCount} </Text>
          </View>
        </TouchableWithoutFeedback>
        <TouchableWithoutFeedback onPress={goToComments}>
          <View style={styles.icon}>
            <Icon name="comment" size={23} color="black"/>
          </View>
        </TouchableWithoutFeedback>
      </View>
      <View style={styles.text}>
        <Text style={styles.title}>{title}</Text>
        <CaptionComponent contents={caption} />
      </View>
    </View>
  );
}
