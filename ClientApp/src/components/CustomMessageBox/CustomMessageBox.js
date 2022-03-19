import { View, Text, StyleSheet } from 'react-native'
import React from 'react'

const CustomMessageBox = ({text}) => {
  return (
    <View>
        <Text style={styles.message}>{text}</Text>
    </View>
  )
}

const styles = StyleSheet.create({
    message: {
        textAlign: "center",
        fontSize: 13,
        color: 'red'
    }
})

export default CustomMessageBox