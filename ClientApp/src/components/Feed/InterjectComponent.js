import React from 'react';
import { Text } from 'react-native';

export default function InterjectComponent({ text }) {

  const styles = {
    interject: {
      fontSize: 19,
      fontWeight: 'bold'
    }
  }
  return (
    <Text style={styles.interject}>{text}</Text>
  )
}