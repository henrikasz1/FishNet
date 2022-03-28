import React from 'react';
import { View, Image, StatusBar, Text, TouchableOpacity } from 'react-native';
import CaptionComponent from './CaptionComponent';

export default function Block({ title, photo, caption, onClick }) {
  const styles = {
    container: {
      flex: 1,
      marginTop: StatusBar.currentHeight || 0,
    },
    item: {
      backgroundColor: '#c9fffd',
      padding: 20,
      marginVertical: 8,
      marginHorizontal: 16,
    },
    title: {
      fontSize: 19,
      fontWeight: 'bold'
    },
    caption: {
      fontSize: 14,
      padding: '1%'
    },
    cardImage: {
      width: '100%',
      minHeight: '30%'
    }
  };

  console.log(photo);
  return (
    <View style={styles.item} onPress={onClick}>
      <Text style={styles.title}>{title}</Text>
      {photo && <Image source={photo} style={styles.cardImage}/>}
      <CaptionComponent contents={caption} />
    </View>
  );
}
