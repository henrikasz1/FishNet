import { View, Text, StyleSheet, ActivityIndicator, ScrollView } from 'react-native'
import React, {useEffect, useState} from 'react'
import ProfileHeader from '../../components/ProfileHeader';
import axios from 'axios';
import { BaseUrl } from '../../components/Common/BaseUrl';
import { useNavigation } from '@react-navigation/native';
import UserPhoto from '../../components/UserPhoto';
import { MenuProvider } from 'react-native-popup-menu';

const PhotosScreen = ({ route }) => {
  const navigation = useNavigation();
  const getUserPhotos = `${BaseUrl}/api/userphoto/getall/${route.params.userId}`;
  const getUserById = `${BaseUrl}/api/user/getbyid/${route.params.userId}/`;
  const [loading, setLoading] = useState(true);
  const [photos, setPhotos] = useState([]);
  const [user, setUser] = useState({});

  const formatName = () => {
    var name = route.params.name + "'s Photos";

    return name;
  }

  useEffect(() => {
    // console.disableYellowBox = true;
    
    const getData = async () => {

      await axios
        .get(getUserPhotos)
        .then(response => {
          let i = 0
          var ph = response.data
          response.data.forEach((photo, i) => {
            if (photo.id == route.params.firstPhotoId)
            {
              ph.splice(i, 1)
              ph.unshift(photo)
              setPhotos(ph)
            }
          })
          setPhotos(response.data);
          changeSequenceOfPhotos();
          setLoading(false);
        })
        .catch(err => {
          console.log(err);
          setLoading(false)
        })

      await axios
        .get(getUserById)
        .then(response => {
          setUser(response.data);
        })
        .catch(err => {
          console.log(err);
        })
    }
    getData();

  }, [loading])


  return (
    <MenuProvider>
    <View style={styles.container}>
      <View>
        <ProfileHeader
          onPressBack={() => navigation.pop() }
          name={formatName()}
        />
      </View>
      {loading ?
        <View style={styles.activityIndicator}>
          <ActivityIndicator size="large" color="#3B71F3" />
        </View>
        :
        <ScrollView>
          <View>
          {photos.map((photo, key) => (
              <View style={styles.block}>
                <UserPhoto
                  key={key}
                  id={photo.id}
                  url={photo.url}
                  body={photo.body}
                  userId={route.params.userId}
                  likesCount={photo.likesCount}
                  name={user.name}
                  lastName={user.lastName}
                  mainUserPhotoUrl={user.mainUserPhotoUrl}
                  currentUserId={route.params.currentUserId}
                />
              </View>))}
          </View>
        </ScrollView>}
    </View>
    </MenuProvider>
  )
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    width: '100%',
    backgroundColor: '#e0e0e0',
  },
  activityIndicator: {
    flex: 1,
    alignItems: 'center',
    justifyContent: 'center'
  },
  block: {
    marginBottom: '2%'
  }
})

export default PhotosScreen