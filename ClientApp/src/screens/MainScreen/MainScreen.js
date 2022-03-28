import { View, Text, Appearance, ScrollView, StyleSheet } from 'react-native'
import Header from '../../components/Header';
import React from 'react'
import axios from 'axios';
import NoMorePostsComponent from '../../components/Feed/NoMorePostsComponent';
import Block from '../../components/Feed/FeedBlock';

const MainScreen = () => {
  const styles = StyleSheet.create({
    container: {
      flex: 1
    },
  });
    // TBD
  return (
    <View style={styles.container}>
      <Header/>
      {/* <Text>
        {axios.defaults.headers.common.Authorization}
      </Text> */}
      <ScrollView style={styles.container}>
        <Block
          title="Money == happiness?"
          photo={{uri: 'https://thumbs.dreamstime.com/b/illustration-chimp-chimpanzee-monkey-ape-wildlife-animal-africa-isolated-white-png-file-available-chimp-165389958.jpg'}}
          caption="Money does buy happiness. Money equals freedom, the highest form of happiness. Money equals fishing rods and worms. The more you have, the more fish you catch, the more pleasurable life is."
          onClick={() => {}}
        />
        <Block
          title="Money == happiness?"
          photo={{uri: 'https://images.unsplash.com/photo-1574781330855-d0db8cc6a79c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8ZmlzaGVybWFufGVufDB8fDB8fA%3D%3D&w=1000&q=80'}}
          caption="Money does buy happiness. Money equals freedom, the highest form of happiness. Money equals fishing rods and worms. The more you have, the more fish you catch, the more pleasurable life is."
          onClick={() => {}}
        />
        <Block
          title="Money == happiness?"
          photo={{uri: 'https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Gorille_des_plaines_de_l%27ouest_%C3%A0_l%27Espace_Zoologique.jpg'}}
          caption="Money does buy happiness. Money equals freedom, the highest form of happiness. Money equals fishing rods and worms. The more you have, the more fish you catch, the more pleasurable life is."
          onClick={() => {}}
        />
        <Block
          title="Money == happiness?"
          photo={{uri: 'https://upload.wikimedia.org/wikipedia/commons/thumb/b/bb/Gorille_des_plaines_de_l%27ouest_%C3%A0_l%27Espace_Zoologique.jpg'}}
          caption="Money does buy happiness. Money equals freedom, the highest form of happiness. Money equals fishing rods and worms. The more you have, the more fish you catch, the more pleasurable life is."
          onClick={() => {}}
        />
        <NoMorePostsComponent />
      </ScrollView>
    </View>
  );
}

export default MainScreen;