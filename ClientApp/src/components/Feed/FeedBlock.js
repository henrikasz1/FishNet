import React, { useState } from 'react';
import { View, Image, StatusBar, Text, TouchableOpacity } from 'react-native';
import CaptionComponent from './CaptionComponent';
import CarouselComponent from './CarouselComponent';
import { BaseUrl } from '../Common/BaseUrl';
import axios from 'axios';

import DefaultUserPhoto from '../../../assets/images/default-user-image.png'

export default function Block({ title, photo, caption, userId, postId, likesCount }) {

  const [profilePicture, setProfilePicture] = useState(null);
  const [userName, setUserName] = useState('');
  const [loading, setLoading] = useState(true);
  const handleLoad = () => {
    const mainPhotoUrl =  `${BaseUrl}/api/userphoto/getmainphoto/${userId}`;
    const profilePhotoPromise = axios.get(mainPhotoUrl).then(response => {
      if(response.data){
        setProfilePicture(response.data.url);
      }
    });

    const userNameUrl =  `${BaseUrl}/api/user/getname/${userId}`;
    const userNamePromise = axios.get(userNameUrl).then(response => {
      if(response.data){
        setUserName(response.data);
      }
    });

    return Promise.all(profilePhotoPromise, userNamePromise);
  }

  if (loading){
    handleLoad().then(() => setLoading(false));
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
      height: 40,
      width: 40,
      borderRadius: 100
    },
    firstBlock: {
      width: '85%',
    },
    secondBlock: {
      width: '15%',
      flexDirection: 'row',
      paddingBottom: '1%'
    },
    profile: {
      paddingTop: '1%',
      paddingRight: '3%',
      paddingHorizontal: '2%',
      height: 60,
      backgroundColor: 'white',
      alignItems: 'center',
      borderBottomWidth: 0.8,
      borderColor: '#d3d3d3',
      flexDirection: 'row',

      width: '100%',
  },
  };

  //todo make post work without photo

  return (
    <View style={styles.item}>
      <View style={styles.profile}>
        <View style={styles.firstBlock}>
          <Text style={styles.title}>
            {userName || "State not set..."}
          </Text>
        </View>
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
      </View>
      {photo === undefined ? <View></View> : (
        <View style={{ width: '100%' }}>
          <CarouselComponent pics={photo} style={styles.cardImage}/>
        </View>
      )}
      <View style={styles.text}>
        <Text style={styles.title}>{title}</Text>
        <CaptionComponent contents={caption} />
      </View>
      <View>
        <Text>DEV</Text>
        <Text>post {postId}</Text>
        <Text>Likes count {likesCount} </Text>
      </View>
    </View>
  );
}
