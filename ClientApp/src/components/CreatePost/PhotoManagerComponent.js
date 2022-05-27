import React, { useRef } from 'react';
import { Text, View, Image } from 'react-native';
import Photo from '../Photo';


export default function PhotoManagerComponent({ pics: photos }){

  const styles = {
    cardImage: {
      aspectRatio: 1,
      borderRadius: 20,
      width: '100%',
      height: 280,
      marginBottom: '7%',
    },
    container: {
      flex: 1
    },
    photos: {
      marginTop: 2,
      flexDirection: 'row',
      flexWrap: 'wrap'
    },
    font: {
      paddingLeft: '2%',
      fontWeight: 'bold',
      fontSize: 20,
      color: '#232b2b',
      marginVertical: '1%'
    },
    contentContainer: {
      flex: 1,
      paddingVertical: '3%',
      borderBottomWidth: 1,
      borderColor: "#DCDCDC",
      width: '100%'
    },
  }
    
  const carouselRef = useRef(null);
  return (
              <View style={styles.contentContainer}>
                <Text style={styles.font}> Photos </Text>
                {(Array.isArray(photos) && photos.length > 0) &&
                    <View style={styles.photos}>
                      {photos.map((photo, key) => (
                        <View width={"33.3%"}>
                          <Photo
                            id={photo.id}
                            photoUrl={photo.uri}
                            key={key}
                          />
                        </View>
                      ))}
                    </View>}
              </View>
  );
}