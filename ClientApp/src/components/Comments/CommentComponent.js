import React, { useState } from "react";
import { View, Text, Image, StyleSheet, TouchableWithoutFeedback } from "react-native";
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import { BaseUrl } from "../../components/Common/BaseUrl";
import axios from "axios";

import DefaultUserPhoto from '../../../assets/images/default-user-image.png';

const styles = StyleSheet.create({
  container: {
    flex: 1,
  },
  comment: {
    // borderBottomWidth: 1.5,
    borderWidth: 1,
    borderColor: '#ededed',
    marginHorizontal: 5,
    borderRadius: 7,
    paddingBottom: 3,
    backgroundColor: 'white',
    marginVertical: 2
  },
  title: {
    color: 'black',
    fontSize: 15,
    paddingBottom: 4,
    paddingHorizontal: 8,
    textAlign: 'left'
  },
  firstBlock: {
    width: '20%',
    flex: 1,
    flexDirection: 'row',
    alignItems: 'center',
    left: '8%'
  },
  secondBlock: {
    width: '80%',
    flexDirection: 'row',
    paddingBottom: '1%',
    alignItems: 'center',
    paddingLeft: 5
    // backgroundColor: 'orange'
  },
  profile: {
    paddingRight: '3%',
    paddingHorizontal: '1%',
    height: 50,
    // backgroundColor: 'white',
    alignItems: 'center',

    flexDirection: 'row',
    // backgroundColor: 'red',
    width: '100%',
  },
  profileImage: {
    marginLeft: '1%',
    height: 30,
    width: 30,
    borderRadius: 100
  },
  icon: {
    // borderRadius: 100,
    height: 40,
    width: 40,
    alignItems: 'center',
    justifyContent: 'center'
  },
  commentBody: {
    paddingLeft: 10,
    paddingRight: 10,
    paddingBottom: 5
  },
  floatRight: {
    float: 'right'
  }
});

export default function Comment({
  id,
  userId,
  userMainPhoto,
  body,
  likesCount,
  createdAt,
  userName,
  commentLiker
}) {

  const [haveThisCommentLiked, setHaveLikedComment] = useState(false);
  const [loading, setLoading] = useState(true);
  const [likeCount, setLikeCount] = useState(likesCount);

  const handleLoad = async () => {
    await fetchCommentLikes();
  }

  const changeCommentLikes = async () => {
    const url = `${BaseUrl}/api/likes/${haveThisCommentLiked ? 'un' : ''}likecomment/${id}`;
    await axios.post(url);
    await fetchCommentLikes();
  }

  const fetchCommentLikes = async () => {
    const url = `${BaseUrl}/api/likes/commentlikedby/${id}`;
    const usersLiked = (await axios.get(url)).data;
    //users who have liked this comment
    if (usersLiked && Array.isArray(usersLiked)) {
      setLikeCount(usersLiked.length);
      const likers = usersLiked.map(({ userId }) => userId);
      setHaveLikedComment(likers.includes(commentLiker));
    }
  }

  if (loading) {
    handleLoad().then(() => setLoading(false));
  }

  return (
    <View style={{...styles.container, ...styles.comment}}>
      <View style={styles.profile}>
        <View style={styles.secondBlock}>
          {
          userMainPhoto !== undefined && userMainPhoto.includes('http') ?
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

           <Text style={styles.title}>
            {userName}
          </Text>
        </View>

        <View style={styles.firstBlock}>
          <TouchableWithoutFeedback onPress={changeCommentLikes}>

            <View style={{...styles.icon, ...styles.floatRight}}>
              <Icon name="heart" size={17} color={haveThisCommentLiked ? "crimson" : "#2d2d2d"} />
            </View>

          </TouchableWithoutFeedback>

          <View style={styles.textBody}>
              <Text>{likeCount}</Text>
          </View>

        </View>
      </View>
      <View style={styles.commentBody}>
        <Text>{body}</Text>
      </View>
    </View>
  );
}
