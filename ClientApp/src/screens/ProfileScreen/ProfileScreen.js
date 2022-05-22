import { View, Text, ScrollView, StyleSheet, Image } from 'react-native'
import React, { useEffect, useState} from 'react'
import Header from '../../components/Header';
import Footer from '../../components/Footer';
import axios from 'axios';
import { BaseUrl } from '../../components/Common/BaseUrl';
import DeadFish from '../../../assets/images/deadFish.png';
import DefaultUserPhoto from '../../../assets/images/default-user-image.png'
import { useNavigation } from '@react-navigation/native';
import ProfileHeader from '../../components/ProfileHeader';

const ProfileScreen = ({ route }) => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);
  const [loading, setLoading] = useState(true)
  const getUserById = `${BaseUrl}/api/user/getbyid/${route.params.userId}`;
  const getCurrentUserId = `${BaseUrl}/api/user/getuserid`;
  const [results, setResults] = useState({})
  const [currentUserId, setCurrentUserId] = useState("");

  const onPressHome = () => {
    navigation.navigate("MainScreen")
  }

  const onPressProfile = () => {
      if (route.params.pop == undefined && currentUserId !== route.params.userId)
      {
        navigation.push("ProfileScreen", {currentBackScreen: route.params.currentBackScreen, backScreen: route.params.backScreen, pop: true, userId: currentUserId});
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

  const backScreenHelper = () => {
    // it's because I can't pass currentBackScreen directly
    if (route.params.pop == true)
    {
      navigation.pop()
    }
    else if (route.params.currentBackScreen == "SearchScreen")
    {
      navigation.navigate("SearchScreen", {backScreen: route.params.backScreen})
    }
    else
    {
      navigation.navigate(route.params.currentBackScreen)
    }
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

  useEffect(() => {

    axios
    .get(getCurrentUserId)
    .then(response => {
      setCurrentUserId(response.data);
    })
    .catch(err => {
      console.log(err)
    })

    axios
      .get(getUserById)
      .then(response => {
        setResults(response.data)
      })
      .catch(err => {
        console.log(err)
      })
      // console.log(results)
  }, [])

  return (

    <View style={styles.container}>
      <ProfileHeader
        onPressBack={() => backScreenHelper() }
        name={formatName()}
      />
      <ScrollView style={styles.container} ref={scrollRef}>
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

        <View>
          <Text> public </Text>
        </View>
      </ScrollView>

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
      alignItems: 'center'
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
      fontWeight: 'bold'
    }
})

export default ProfileScreen