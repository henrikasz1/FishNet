import { View, Text } from 'react-native'
import Header from '../../components/Header';
import React from 'react'
import axios from 'axios';

const MainScreen = () => {
    // TBD
  return (
    <View>
      <Header/>
      <Text>
        {axios.defaults.headers.common['Authorization']}
      </Text>
    </View>
  )
}

export default MainScreen