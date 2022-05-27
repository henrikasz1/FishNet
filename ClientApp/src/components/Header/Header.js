import { View, Text, StyleSheet, Image, TouchableWithoutFeedback} from 'react-native'
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import Logo from '../../../assets/images/FishNetLogo.png';
import DefaultUserPhoto from '../../../assets/images/default-user-image.png'
import React, {useState, useEffect } from 'react'
import { BaseUrl } from '../Common/BaseUrl'
import axios from 'axios';

function Header({first, second}) {

  const [profileImage, setProfileImage] = useState(undefined);
  const [userId, setUserId] = useState('');
  const userIdUrl = `${BaseUrl}/api/user/getuserid`;

  useEffect(() => {
    axios.get(userIdUrl).then(res => setUserId(res.data));

    const profileImageUrl = `${BaseUrl}/api/userphoto/getmainphoto/${userId}`;

    axios
      .get(profileImageUrl)
      .then(response => setProfileImage(response.data.url))
      .catch(err => console.log(err))
  })

  return (
    <View style={styles.header}>

        <View style={styles.firstBlock} >
          <Image source={Logo} style={styles.logo} />
        </View>

        <View style={styles.secondBlock}>

          <View style={styles.icon}>
            <Icon name="search" size={23} onPress={()=>first}/>
          </View>

          {profileImage !== undefined ?
          <TouchableWithoutFeedback onPress={second}>
            <Image
              source={{ uri: profileImage }}
              style={styles.image}
            />
          </TouchableWithoutFeedback>
          :
          <TouchableWithoutFeedback onPress={second}>
            <Image
              source={DefaultUserPhoto}
              style={styles.image}
              onPress={second}
            />
          </TouchableWithoutFeedback>}
        </View>

    </View>
  )
}

const styles = StyleSheet.create({
    header: {
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
    logo: {
        width: 120,
        height: 60
    },
    firstBlock: {
        width: '75%'
    },
    secondBlock: {
      width: '25%',
      flex: 1,
      flexDirection: 'row',
      paddingBottom: '1%'
    },
    icon: {
      backgroundColor: '#eaf1f8',
      borderRadius: 100,
      height: 40,
      width: 40,
      alignItems: 'center',
      justifyContent: 'center'
    },
    image: {
      marginLeft: '5%',
      height: 40,
      width: 40,
      borderRadius: 100
    }
})

export default Header;