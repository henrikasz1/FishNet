import React from 'react';
import {Text, View, Image, StyleSheet} from 'react-native';

const styles = StyleSheet.create({
  product: {
    width: '50%'
  },
  image: {
    flex: 1,
    aspectRatio: 1,
    width: '100%'
  }
});

export default function Product({description, title, price, location, photos, shopId}) {
  return (<View style={styles.product}>
    {photos && photos[0] && <Image style={styles.image} source={{uri: photos[0].url}} />}
    <Text>{title}</Text>
    <Text>{location}</Text>
    <Text>{price}</Text>
  </View>)
}

