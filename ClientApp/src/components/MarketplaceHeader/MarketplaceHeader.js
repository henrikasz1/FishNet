import { View, Text, StyleSheet, Image, TouchableWithoutFeedback} from 'react-native'
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import React, {useState, useEffect } from 'react'

function MarketplaceHeader({first}) {

  return (
    <View style={styles.header}>
        <View style={styles.firstBlock} >
          <Text style={styles.baseText}>FishNet Marketplace</Text>
        </View>
        <View style={styles.secondBlock}>
          <View style={styles.icon}>
            <Icon name="search" size={23} onPress={()=>first()}/>
          </View>
        </View>
    </View>
  )
}

const styles = StyleSheet.create({
    header: {
        paddingTop: '1%',
        paddingRight: '3%',
        paddingHorizontal: '2%',
        height: 60,
        backgroundColor: 'white',
        alignItems: 'center',
        borderBottomWidth: 0.8,
        borderColor: '#d3d3d3',
        flexDirection: 'row',
        width: '100%',
    },
    baseText: {
        fontWeight: 'bold',
        fontSize: 20
    },
    firstBlock: {
        width: '90%'
    },
    secondBlock: {
      width: '25%',
      flex: 1,
      flexDirection: 'row',
      paddingBottom: '1%'
    },
    icon: {
      backgroundColor: '#eaf1f8',
      borderRadius: 100,
      height: 40,
      width: 40,
      alignItems: 'center',
      justifyContent: 'center'
    },
})

export default MarketplaceHeader;