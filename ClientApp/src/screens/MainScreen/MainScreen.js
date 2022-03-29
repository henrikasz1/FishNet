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

  //PREPARE TEST DATA FOR FRONT-END
  let data = [];
  for (let i = 0; i < 10; i++){
    data.push({
      title: 'Money == happiness?',
      photo: {
        uri: 'https://images.unsplash.com/photo-1574781330855-d0db8cc6a79c?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxzZWFyY2h8MXx8ZmlzaGVybWFufGVufDB8fDB8fA%3D%3D&w=1000&q=80'
      },
      caption: 'Money does buy happiness. Money equals freedom, the highest form of happiness. Money equals fishing rods and worms. The more you have, the more fish you catch, the more pleasurable life is.',
    });
  }

  //PREPARE TEST DATA 2
  data[1].photo.uri = 'https://thumbs.dreamstime.com/z/old-fisherman-9941779.jpg';
  data[2].photo.uri = 'https://www.stateoftheapes.com/wp-content/uploads/2018/08/BR-leaveinmouth-March2014-Martha-Robbins-940x705.jpg';

  //FETCH REAL DATA FROM DOT NET

  return (
    <View style={styles.container}>
      <Header/>
      {/* <Text>
        {axios.defaults.headers.common.Authorization}
      </Text> */}
      <ScrollView style={styles.container}>
        {data.map(({ title, photo, caption }, index) => (
          <Block
            title={title}
            photo={photo}
            caption={caption}
            key={index}
          />
        ))}
      </ScrollView>
    </View>
  );
}

export default MainScreen;