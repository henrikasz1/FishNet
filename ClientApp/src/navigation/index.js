import { View, Text } from 'react-native'
import React from 'react'
import {NavigationContainer} from '@react-navigation/native'
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import SignInScreen from '../screens/SignInScreen';
import SignUpScreen from '../screens/SignUpScreen';
import MainScreen from '../screens/MainScreen';

const Stack = createNativeStackNavigator();

const Navigation = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{headerShown: false}}>
          <Stack.Screen name='SignIn' component={SignInScreen} />
          <Stack.Screen name='SignUp' component={SignUpScreen} />
          <Stack.Screen name='MainScreen' component={MainScreen} />
      </Stack.Navigator>
    </NavigationContainer>
  )
}

export default Navigation