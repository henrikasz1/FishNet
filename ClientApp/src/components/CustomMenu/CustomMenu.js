import React from 'react';
import Icon from 'react-native-vector-icons/dist/Entypo';
import { Text, View } from 'react-native';
import {
    Menu,
    MenuOptions,
    MenuOption,
    MenuTrigger,
  } from 'react-native-popup-menu';

const CustomMenu = ({changeMainUserPhoto, deleteUserPhoto}) => (
    <View>
    <Menu>
      <MenuTrigger>
        <View>
            <Icon name="dots-three-vertical" size={20} color={'#2d2d2d'} />
        </View>
      </MenuTrigger>
      <MenuOptions optionsContainerStyle={{ marginTop: 30, color: 'black' }}>
        <MenuOption onSelect={changeMainUserPhoto} text='Change photo to main' />
        <MenuOption onSelect={() => alert(`Not called`)} text='Edit' />
        <MenuOption onSelect={deleteUserPhoto} >
          <Text style={{color: 'red'}}>Delete</Text>
        </MenuOption>
      </MenuOptions>
    </Menu>
  </View>
);

export default CustomMenu;