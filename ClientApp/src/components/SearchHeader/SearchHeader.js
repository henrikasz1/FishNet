import { View, Text, StyleSheet, TextInput, Image, TouchableWithoutFeedback} from 'react-native';
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import React, {useState, useEffect } from 'react';
import { BaseUrl } from '../Common/BaseUrl';
import axios from 'axios';
import CustomInput from '../CustomInput';

function SearchHeader({value, setValue, onPressBack}) {

  return (
    <View style={styles.header}>
        <View style={styles.root}>
            <View style={styles.icon}>
                <Icon name={"chevron-left"} size={25} color={"#353839"} onPress={onPressBack}/>
            </View>

            <View style={styles.textContainer}>
                <TextInput
                    value={value}
                    onChangeText={setValue}
                    style={styles.container}
                    placeholder={"Search"}
                />
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
        width: '90%'
    },
    container: {
        height: '70%',
        borderColor: '#E8E8E8',
        borderWidth: 1,
        borderRadius: 25,
        paddingHorizontal: 20,
        width: '100%',
    },
})

export default SearchHeader;