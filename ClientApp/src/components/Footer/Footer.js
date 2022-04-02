import { View, Text, StyleSheet } from 'react-native'
import React from 'react'
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import { useNavigation } from '@react-navigation/native';

function Footer({
    homeC,
    profileC,
    shopC,
    eventC,
    groupsC,
    onPressHome,
    onPressProfile,
    onPressShop,
    onPressEvent,
    onPressGroup}) {

  const navigation = useNavigation();

  return (
    <View style={styles.footer}>
        <Icon name={"home"} size={30} color={homeC ?? "#353839"} onPress={onPressHome} />
        <Icon name={"user-circle-o"} size={30} color={profileC ?? "#353839"} onPress={onPressProfile} />
        <Icon name={"shopping-bag"} size={30} color={shopC?? "#353839"} onPress={onPressShop} />
        <Icon name={"calendar-plus-o"} size={30} color={eventC ?? "#353839"} onPress={onPressEvent}/>
        <Icon name={"group"} size={30} color={groupsC ?? "#353839"} onPress={onPressGroup}/>
    </View>
  )
}

const styles = StyleSheet.create({
    footer: {
        paddingHorizontal: '5%',
        height: 60,
        backgroundColor: 'white',
        alignItems: 'center',
        borderTopWidth: 0.7,
        borderColor: '#d3d3d3',
        flexDirection: 'row',
        justifyContent: 'space-between',
        width: '100%',
    },
})

export default Footer