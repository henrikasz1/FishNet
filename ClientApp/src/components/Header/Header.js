import { View, Text, StyleSheet, Image } from 'react-native'
import Icon from 'react-native-vector-icons/dist/SimpleLineIcons';
import Logo from '../../../assets/images/FishNetLogo.png';
import React from 'react'

const Header = () => {
  return (
    <View style={styles.header}>
        <Image source={Logo} style={styles.logo} />
        <Text>Header</Text>
        <Text>Header</Text>
    </View>
  )
}

const styles = StyleSheet.create({
    header: {
        paddingTop: '1%',
        paddingRight: '3%',
        height: '23%',
        backgroundColor: 'white',
        borderBottomWidth: 0.2,
        borderColor: 'grey',
        flexDirection: 'row',
        justifyContent: 'space-between',
    },
    logo: {
        width: '40%',
        height: '100%'
    }
})

export default Header