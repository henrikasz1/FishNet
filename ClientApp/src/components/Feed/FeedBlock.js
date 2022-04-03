import React from 'react';
import { View, Image, StatusBar, Text, TouchableOpacity } from 'react-native';
import CaptionComponent from './CaptionComponent';
import CarouselComponent from './CarouselComponent';

export default function Block({ title, photo, caption, userId, postId, likesCount }) {
  const styles = {
    // container: {
    //   flex: 1,
    //   marginTop: StatusBar.currentHeight || 0,
    // },
    item: {
      color: 'black',
      backgroundColor: 'white',
      paddingVertical: 6,
      marginBottom: 5,
    },
    title: {
      color: '#353839',
      fontSize: 19,
      fontWeight: 'bold',
      paddingBottom: 4,
      textAlign: 'left'
    },
    cardImage: {
      width: '100%',
      aspectRatio: 1
    },
    text: {
      paddingHorizontal: '3%',
      marginBottom: '3%'
    }
  };

  //todo make post work without photo

  return (
    <View style={styles.item}>
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
        <Text>user {userId}</Text>
        <Text>post {postId}</Text>
        <Text>Likes count {likesCount} </Text>
      </View>
    </View>
  );
}
