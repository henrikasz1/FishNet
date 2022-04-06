import React, { useState } from "react";
import { View, Text, Image, StyleSheet } from "react-native";
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import { BaseUrl } from "../../components/Common/BaseUrl";
import axios from "axios";

import DefaultUserPhoto from '../../../assets/images/default-user-image.png';

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  title: {
    color: 'black',
    fontSize: 19,
    // fontWeight: 'bold',
    paddingBottom: 4,
    paddingHorizontal: 8,
    textAlign: 'left'
  },
  firstBlock: {
    width: '85%',
    flexDirection: 'row'
  },
  secondBlock: {
    // width: '15%',
    flexDirection: 'row',
    paddingBottom: '1%',
    alignItems: 'center'
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
  profileImage: {
    marginLeft: '5%',
    height: 35,
    width: 35,
    borderRadius: 100
  },
  icon: {
    // borderRadius: 100,
    height: 40,
    width: 40,
    alignItems: 'center',
    justifyContent: 'center'
  },
});

export default function Comment({
  id,
  userId,
  userMainPhoto,
  body,
  likesCount,
  createdAt,
  userName
}) {
  return (
    <View style={styles.container}>
      <View style={styles.profile}>
        <View style={styles.secondBlock}>
          {userMainPhoto !== undefined ?
            <Image
              source={{ uri: userMainPhoto }}
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
            {userName || "//TODO import user name"}
          </Text>
          <View style={styles.icon}>
            <Icon name="heart" size={17} color={true ? "crimson" : "black"} />
          </View>
            <Text>{1} </Text>
        </View>
      </View>
      <Text>DEV {id}</Text>
      <Text>{body}</Text>
      <Text>LIKES {likesCount}</Text>
    </View>
  );
}
