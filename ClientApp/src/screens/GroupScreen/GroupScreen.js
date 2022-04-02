import { View, Text, ScrollView, StyleSheet } from 'react-native'
import React from 'react'
import Header from '../../components/Header';
import Footer from '../../components/Footer';
import { useNavigation } from '@react-navigation/native';

const GroupScreen = () => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);

  const onPressHome = () => {
    navigation.navigate("MainScreen")
  }

  const onGroupScreen = () => {
    scrollRef.current?.scrollTo({
        y: 0,
        animated: true,
      });
  }

  const onPressShop = () => {
      navigation.navigate("ShopScreen")
  }

  const onPressProfile = () => {
      navigation.navigate("ProfileScreen")
  }

  const onPressEvent = () => {
      navigation.navigate("EventScreen")
  }


  return (

    <View style={styles.container}>
      <Header/>
      <ScrollView style={styles.container} ref={scrollRef}>
        <Text> Group Screen </Text>
      </ScrollView>

      <Footer
        style={styles.footer}
        groupsC="#3B71F3"
        onPressHome={onPressHome}
        onPressProfile={onPressProfile}
        onPressShop={onPressShop}
        onPressEvent={onPressEvent}
        onGroupScreen={onGroupScreen}
      />

    </View>
  )
}

const styles = StyleSheet.create({
    container: {
        flex: 1
    },
})

export default GroupScreen