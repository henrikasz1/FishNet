import React from 'react';
import {Text, View, Image, StyleSheet} from 'react-native';

const styles = StyleSheet.create({
  image: {
    flex: 1,
    aspectRatio: 1,
  }
});

export default function Photo({photoUrl, id}) {
  return (<View>
    {photoUrl && <Image style={styles.image} source={{uri: photoUrl}} />}
  </View>)
}

