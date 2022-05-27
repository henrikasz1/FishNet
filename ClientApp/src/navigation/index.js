import { View, Text } from 'react-native'
import React from 'react'
import {NavigationContainer} from '@react-navigation/native'
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import SignInScreen from '../screens/SignInScreen';
import SignUpScreen from '../screens/SignUpScreen';
import MainScreen from '../screens/MainScreen';
import InitialPhotoScreen from '../screens/InitialPhotoScreen';
import ProfileScreen from '../screens/ProfileScreen';
import ShopScreen from '../screens/ShopScreen';
import EventScreen from '../screens/EventScreen';
import GroupScreen from '../screens/GroupScreen';
import CommentsScreen from '../screens/CommentsScreen';
import SearchScreen from '../screens/SearchScreen';
import PostAdvertsScreen from '../screens/PostAdvertsScreen';
import CreatePostScreen from '../screens/CreatePostScreen';
import PhotosScreen from '../screens/PhotosScreen';
import EditProfileScreen from '../screens/EditProfileScreen';

const Stack = createNativeStackNavigator();

const Navigation = () => {
  return (
    <NavigationContainer>
      <Stack.Navigator screenOptions={{headerShown: false}}>
          <Stack.Screen name='SignIn' component={SignInScreen} />
          <Stack.Screen name='SignUp' component={SignUpScreen} />
          <Stack.Screen name='MainScreen' component={MainScreen} />
          <Stack.Screen name='InitialPhotoScreen' component={InitialPhotoScreen} />
          <Stack.Screen name='ProfileScreen' component={ProfileScreen} />
          <Stack.Screen name='ShopScreen' component={ShopScreen} />
          <Stack.Screen name='EventScreen' component={EventScreen} />
          <Stack.Screen name='GroupScreen' component={GroupScreen} />
          <Stack.Screen name='CommentsScreen' component={CommentsScreen} />
          <Stack.Screen name='SearchScreen' component={SearchScreen} />
          <Stack.Screen name='CreatePostScreen' component={CreatePostScreen} />
          <Stack.Screen name='PhotosScreen' component={PhotosScreen} />
          <Stack.Screen name="PostAdvertsScreen" component={PostAdvertsScreen} />
          <Stack.Screen name='EditProfileScreen' component={EditProfileScreen} />
      </Stack.Navigator>
    </NavigationContainer>
  )
}

export default Navigation