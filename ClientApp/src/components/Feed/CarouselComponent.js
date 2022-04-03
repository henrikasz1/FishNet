import React, { useRef } from 'react';
import { Text, View, Image } from 'react-native';
import { SimpleCarousel } from '@wecraftapps/react-native-simple-carousel';


export default function CarouselComponent({ pics }){

  const styles = {
    cardImage: {
      width: '100%',
      aspectRatio: 1
    },
  }
    
  const carouselRef = useRef(null);
  return (
    <SimpleCarousel ref={carouselRef} style={{width: '100%'}}>
      {
        Array.isArray(pics) && pics.map((pic, key) => (
          <View style={{ width: '100%', height: '100%', justifyContent: 'center', alignItems: 'center'}} key={key}>
            <Image source={{uri: pic.url}} style={styles.cardImage} />
          </View>
        ))
      }
    </SimpleCarousel>
  );
}