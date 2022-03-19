import { View, Text } from 'react-native'
import React from 'react'
import axios from 'axios';

const MainScreen = () => {
    // TBD
  return (
    <View>
      <Text>
        {axios.defaults.headers.common['Authorization']}
      </Text>
    </View>
  )
}

export default MainScreen