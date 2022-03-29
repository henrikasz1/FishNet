import React from 'react';
import { View, Image, StatusBar, Text, TouchableOpacity } from 'react-native';
import CaptionComponent from './CaptionComponent';

export default function Block({ title, photo, caption }) {
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

  return (
    <View style={styles.item}>
      <View style={styles.text}>
        <Text style={styles.title}>{title}</Text>
        <CaptionComponent contents={caption} />
      </View>
      {photo && (
        <Image source={photo} style={styles.cardImage}/>
      )}
    </View>
  );
}
