import React, { useRef } from 'react';
import { Text, View, Image } from 'react-native';
import { SimpleCarousel } from '@wecraftapps/react-native-simple-carousel';
import DefaultUserPhoto from '../../../assets/images/default-user-image.png'


export default function PhotoManagerComponent({ pics }){

  const styles = {
    cardImage: {
      aspectRatio: 1,
      borderRadius: 20,
      width: '100%',
      height: 280,
      marginBottom: '7%',
    }
  }
    
  const carouselRef = useRef(null);
  return (
    <SimpleCarousel ref={carouselRef} style={{width: '85%'}}>
      {
        (Array.isArray(pics) && pics.length > 0) ? pics.map((pic, key) => (
          <View style={{ width: '100%', height: '100%', justifyContent: 'center', alignItems: 'center'}} key={key}>
            <Image source={{uri: pic.uri}} style={styles.cardImage} />
          </View>
        )) : [<View style={{ width: '100%', height: '100%', justifyContent: 'center', alignItems: 'center'}} >
            <Image
                source={DefaultUserPhoto}
                style={styles.cardImage}
            />
        </View>]
      }
    </SimpleCarousel>
  );
}