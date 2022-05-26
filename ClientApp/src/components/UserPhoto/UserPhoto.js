import { View, Text, Image, StyleSheet, TouchableWithoutFeedback } from 'react-native'
import React, {useState} from 'react'
import CustomMenu from '../../components/CustomMenu';
import DefaultUserPhoto from '../../../assets/images/default-user-image.png'
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import axios from 'axios';
import CaptionComponent from '../../components/Feed/CaptionComponent';
import { BaseUrl } from '../Common/BaseUrl';
import { useNavigation } from '@react-navigation/native';

const UserPhoto = ({id, url, body, userId, likesCount, name, lastName, mainUserPhotoUrl, currentUserId }) => {
  const navigation = useNavigation();

  const [lastTap, setLastTap] = useState(null);
  const [likes, setLikes] = useState(0);
  const [hasMyLike, setHasMyLike] = useState(false);
  const [selfLikesCount, setLikesCount] = useState(likesCount);
  const [loading, setLoading] = useState(true);
  
  const handleDoubleTap = () => {
    const now = Date.now();
    const DOUBLE_PRESS_DELAY = 300;
    if (lastTap && (now - lastTap) < DOUBLE_PRESS_DELAY) {
        setLastTap(null);
        changePhotoLikes();
    }
    else
    {
        setLastTap(now);
    }
  }

  const changePhotoLikes = async () => {
    const url = `${BaseUrl}/api/likes/${hasMyLike ? 'un' : ''}likephoto/${id}`;
    await axios.post(url);
    await loadLikes();
  }

  const loadLikes = async () => {
    const haveLikedUrl = `${BaseUrl}/api/likes/photolikedby/${id}`;
    return axios.get(haveLikedUrl).then(response => {
      if (response.data && Array.isArray(response.data)) {
        const likerIds = response.data.map(r => r.userId);
        setHasMyLike(likerIds.includes(currentUserId));
        setLikesCount(likerIds.length);
      }
    }).catch(ignore => {});
  }

  const changeMainUserPhoto = async (imageId) => {
    const changeMainUserPhoto = `${BaseUrl}/api/userphoto/changemainuserphoto/${imageId}`;

    await axios
      .put(changeMainUserPhoto)
      .catch(err => {
        console.log(err);
      })

      navigation.push("MainScreen");
  }

  const deletePhoto = async (imageId) => {
    const deleteUserPhoto = `${BaseUrl}/api/userphoto/delete/${imageId}`;

    await axios
      .delete(deleteUserPhoto)
      .catch(err => {
        console.log(err);
      })

    navigation.push("MainScreen");
  }

  if (loading){
    loadLikes().then(() => setLoading(false));
  }


  return (
    <View style={styles.container}>
        <View style={styles.photoHeader}>
            {mainUserPhotoUrl !== undefined && mainUserPhotoUrl.includes('http') ?
              <Image
                source={{ uri: mainUserPhotoUrl }}
                style={ styles.userPhoto }
              />
              :
              <Image
                source={ DefaultUserPhoto }
                style={ styles.userPhoto }
              />
            }
            <Text style={styles.font}> {name} {lastName} </Text>
            {currentUserId == userId &&
              <View style={styles.dots}>
                <CustomMenu
                    style={styles.dots}
                    changeMainUserPhoto={() => changeMainUserPhoto(id)}
                    deleteUserPhoto={() => deletePhoto(id)}    
                />
              </View>}
        </View>
        <TouchableWithoutFeedback onPress={handleDoubleTap}>
            <Image
                source={{ uri: url }}
                style={styles.photo}
            />
        </TouchableWithoutFeedback>

        <View style={styles.text}>
            <CaptionComponent contents={body} />
        </View>

        <View style={styles.row}>

            <TouchableWithoutFeedback onPress={changePhotoLikes}>
            {hasMyLike ?
                <View style={styles.icon}>
                <Icon name="heart" size={22} color={"crimson"} />
                <Text>{selfLikesCount} </Text>
                </View>
                :
                <View style={styles.icon}>
                <Icon name="heart-o" size={22} color={"black"} />
                <Text>{selfLikesCount} </Text>
                </View>
            }
            </TouchableWithoutFeedback>

        </View>
    </View>
  )
}

const styles = StyleSheet.create({
    container:{
        flex: 1,
        marginBottom: 2,
        backgroundColor: 'white',
    },
    photo: {
        width: '100%',
        aspectRatio: 1
    },
    row: {
        flexDirection: 'row',
        width: '100%',
        height: 53
    },
    photoHeader: {
        flex: 1,
        paddingHorizontal: '1%',
        height: 55,
        backgroundColor: 'white',
        alignItems: 'center',
        borderBottomWidth: 0.8,
        borderColor: '#d3d3d3',
        flexDirection: 'row',
        width: '100%',
    },
    userPhoto: {
        marginHorizontal: '2%',
        height: 35,
        width: 35,
        borderRadius: 100
    },
    font: {
        color: 'black',
        fontSize: 17,
        paddingBottom: 4,
        paddingHorizontal: 8,
        textAlign: 'left'
      },
    dots: {
        marginLeft: 'auto',
        paddingRight: '3%'
    },
    icon: {
        paddingTop: 8,
        paddingLeft: '3%',
        paddingRight: '3%',
        alignItems: 'center'
    },
    text: {
        marginTop: '3%',
        paddingHorizontal: '3%',
        marginBottom: '3%',
    }
})

export default UserPhoto