import { View, Text, TextInput, StyleSheet } from 'react-native'
import React from 'react'
import Icon from 'react-native-vector-icons/dist/SimpleLineIcons';

const CustomInput = ({value, setValue, placeholder, iconType, secureTextEntry}) => {
  return (
    <View style={styles.root}>
      <View style={styles.icon}>
        <Icon
            // style={styles.icon}
            name={iconType}
            size={30}
        />
      </View>
      <TextInput
        value={value}
        onChangeText={setValue}
        style={styles.container}
        placeholder={placeholder}
        secureTextEntry={secureTextEntry}
      />
    </View>
  )
}

const styles = StyleSheet.create({
    root: {
        flexDirection: 'row',
        width: '100%',
        backgroundColor: 'white',
        marginVertical: '2%'
    },
    container: {
        borderColor: '#E8E8E8',
        borderWidth: 1,
        borderRadius: 5,
        paddingHorizontal: 20,
        width: '85%',
    },
    icon: {
        padding: 10,
        borderColor: '#E8E8E8',
        borderWidth: 1,
        borderRadius: 5,
        width: 55,
        height: '100%',
        alignItems: 'center',
    }
})

export default CustomInput