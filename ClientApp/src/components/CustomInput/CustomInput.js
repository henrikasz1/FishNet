import { View, Text, TextInput, StyleSheet } from 'react-native'
import React from 'react'
import Icon from 'react-native-vector-icons/dist/FontAwesome';

const CustomInput = ({value, setValue, placeholder, iconType, secureTextEntry}) => {
  return (
    <View style={styles.root}>
      <Icon
        style={styles.icon}
        name={iconType}
        size={30}
      />
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
        width: '88%',
    },
    icon: {
        padding: 10,
        paddingLeft: 12,
        borderColor: '#E8E8E8',
        borderWidth: 1,
        borderRadius: 5,
        width: '12%',
        height: '100%',
        alignItems: 'center',
    }
})

export default CustomInput