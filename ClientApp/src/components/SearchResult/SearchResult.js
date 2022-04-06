import { View, Text, Image, StyleSheet } from 'react-native'
import React, { useState, useEffect } from 'react'

const SearchResult = ({id, name, photo, type}) => {


  const [image, setImage] = useState(photo);

  return (
    <View style={styles.root}>
      <View style={styles.imageContainer}>
        <Image source={{uri: photo}} style={styles.photo}/>
      </View>
      <View style={styles.dataContainer}>
        <View style={styles.name}>
            <Text style={styles.text} color={"black"}>{name}</Text>
        </View>
        <View>
            <Text style={styles.text}>{type}</Text>
        </View>
      </View>
    </View>
  )
}

const styles = StyleSheet.create({
    root: {
        backgroundColor: '#FFFFFF',
        height: 60,
        width: '100%',
        borderBottomWidth: 1,
        borderColor: '#d3d3d3',
        flex: 1,
        flexDirection: 'row',
        alignItems: 'center',
    },
    imageConatainer: {
        width: '30%',
    },
    dataContainer: {
        width: '70%',
        flex: 1,
        flexDirection: 'row'
    },
    photo: {
        marginLeft: '12%',
        height: 40,
        width: 40,
        borderRadius: 100,
    },
    text: {
        fontSize: 15,
    },
    name: {
        width: '80%'
    }
})

export default SearchResult