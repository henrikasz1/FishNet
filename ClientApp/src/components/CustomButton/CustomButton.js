import React from 'react'
import { Text, Pressable, StyleSheet } from 'react-native'

function CustomButton({onPress, text, type = "primary", bgColor, fColor}) {
  return (
    <Pressable
      onPress={onPress}
      style={[
        styles.container,
        styles[`container_${type}`],
        bgColor ? {backgroundColor: bgColor} : {}
      ]}>
      <Text
        style={[
          styles.text,
          styles[`text_${type}`],
          fColor ? {color: fColor} : {}
        ]}>
        {text}</Text>
    </Pressable>
  )
}

const styles = StyleSheet.create({
    container: {
      width: '100%',
      padding: 15,
      marginVertical: '2%',
      alignItems: 'center',
      borderRadius: 5,
    },
    container_primary: {
      backgroundColor: '#3B71F3',
    },
    container_tertiary: {},
    text: {
      fontWeight: 'bold',
      color: 'white'
    },
    text_tertiary: {
      color: 'grey'
    }
})

export default CustomButton