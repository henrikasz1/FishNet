import { View, Text, StyleSheet, TouchableWithoutFeedback} from 'react-native';
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import React from 'react';

function ProfileHeader({name, onPressBack, edit}) {

  return (
    <View style={styles.header}>
        <View style={styles.root}>
            <View style={styles.icon}>
                <Icon name={"chevron-left"} size={25} color={"#353839"} onPress={onPressBack}/>
            </View>

            <View style={styles.textContainer}>
                <Text style={styles.text}> {name} </Text>
            </View>
            
            <View style={styles.edit}>
                {edit &&
                <Icon name={"edit"} size={30} color={"#353839"}/>}
            </View>
        </View>
    </View>
  )
}

const styles = StyleSheet.create({
    root: {
        flexDirection: 'row',
        width: '100%',
        backgroundColor: 'white',
        marginVertical: '2%',
        height: '100%'
    },
    text: {
        fontSize: 20,
        fontWeight: 'bold',
        color: 'black'
    },
    header: {
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
    icon: {
        width: '10%',
        height: '100%',
        alignItems: 'center',
        justifyContent: 'center'
    },
    textContainer: {
        justifyContent: 'center',
        paddingLeft: '5%',
    },
    container: {
        height: '70%',
        borderColor: '#E8E8E8',
        borderWidth: 1,
        borderRadius: 25,
        paddingHorizontal: 20,
        width: '100%',
    },
    edit: {
        marginLeft: 'auto',
        justifyContent: 'center',
        alignItems: 'center'
    }
})

export default ProfileHeader;