import { View, Text, StyleSheet, Button, Image, TextInput, ScrollView, ActivityIndicator, useWindowDimensions } from 'react-native'
import React, { useState } from 'react'
import { useNavigation } from '@react-navigation/native';
import { BaseUrl } from '../../components/Common/BaseUrl'
import axios from 'axios';
import Header from '../../components/Header';
import Footer from '../../components/Footer';
import Comment from '../../components/Comments/CommentComponent';
import CommentWriteComponent from '../../components/Comments/CommentWriteComponent';
import Fish from '../../../assets/images/Fish.png';

const styles = StyleSheet.create({
  container: {
      flex: 1
  },
  center: {
    justifyContent: 'center',
    alignItems: 'center'
  }
})

export default function CommentsScreen({ route }) {
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);
  const { postId, commentWriterId } = route.params;
  const [loading, setLoading] = useState(true);
  const [comments, setComments] = useState([]);
  const {height} = useWindowDimensions();

  const onPressHome = () => {
    navigation.navigate("MainScreen");
  }

  const onPressEvent = () => {
    scrollRef.current?.scrollTo({
        y: 0,
        animated: true,
      });
  }

  const onPressShop = () => {
      navigation.navigate("ShopScreen");
  }

  const onPressProfile = () => {
      navigation.navigate("ProfileScreen");
  }

  const onPressGroup = () => {
      navigation.navigate("GroupScreen");
  };

  const handleLoad = async () => {
    const url = `${BaseUrl}/api/comments/getall/${postId}`;
    const commentsData = (await axios.get(url)).data;
    setComments(commentsData);
  };

  if (loading) {
    handleLoad().then(() => setLoading(false));
  }

  return (

    <View style={styles.container}>
      <Header/>
      {comments.length > 0 ? 
        <ScrollView style={styles.container} ref={scrollRef}>
          {comments.map(({ commentId, userId, userMainPhoto, body, likesCount, createdAt, userName }, key) => 
            <Comment 
              id={commentId}
              userId={userId}
              userMainPhoto={userMainPhoto}
              body={body}
              likesCount={likesCount}
              createdAt={createdAt}
              commentLiker={commentWriterId}
              postId={postId}
              userName={userName}
              key={key}
            />
          )}
        </ScrollView>
      : 
        <View style={{...styles.container, ...styles.center}}>
          <Image source={Fish} style={{height: height * 0.25}} resizeMode="contain" />
          <Text> Write something smart! </Text>
        </View >
      }
      <CommentWriteComponent postId={postId} commentWriterId={commentWriterId} reloadFunction={handleLoad} />
      <Footer
        style={styles.footer}
        onPressHome={onPressHome}
        onPressProfile={onPressProfile}
        onPressShop={onPressShop}
        onPressEvent={onPressEvent}
        onPressGroup={onPressGroup}
      />
    </View>
  )
}