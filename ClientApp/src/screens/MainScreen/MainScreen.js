import { View, Text } from 'react-native'
import Header from '../../components/Header';
import React from 'react'
import axios from 'axios';
import NoMorePostsComponent from '../../components/Feed/NoMorePostsComponent';

const MainScreen = () => {
    // TBD
  return (
    <View>
      <Header/>
      <Text>
        {axios.defaults.headers.common['Authorization']}
      </Text>
      <NoMorePostsComponent />
    </View>
  )
}

export default MainScreen