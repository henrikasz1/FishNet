import React from 'react';
import { View, Image, StatusBar, Text, TouchableOpacity } from 'react-native';
import CaptionComponent from './CaptionComponent';

export default function Block({ title, photo, caption }) {
  const styles = {
    container: {
      flex: 1,
      marginTop: StatusBar.currentHeight || 0,
    },
    item: {
      backgroundColor: 'lightgray',
      paddingTop: 6,
      paddingBottom: 6,
      marginVertical: 8,
    },
    title: {
      fontSize: 19,
      fontWeight: 'bold',
      paddingBottom: 4,
      textAlign: 'center'
    },
    caption: {
      fontSize: 14,
      padding: '1%'
    },
    cardImage: {
      width: '100%',
      aspectRatio: 1
    }
  };

  return (
    <View style={styles.item}>
      <Text style={styles.title}>{title}</Text>
      {photo && (
        <Image source={photo} style={styles.cardImage}/>
      )}
      <CaptionComponent contents={caption} />
    </View>
  );
}
