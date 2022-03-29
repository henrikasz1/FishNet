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
        paddingTop: '2%',
        paddingRight: '3%',
        paddingBottom: '2%',
        height: 60,
        backgroundColor: 'white',
        alignItems: 'center',
        borderBottomWidth: 0.2,
        borderColor: 'grey',
        flexDirection: 'row',
        justifyContent: 'space-around'
    },
    logo: {
        width: '40%',
        height: '100%'
    }
})

export default Header;