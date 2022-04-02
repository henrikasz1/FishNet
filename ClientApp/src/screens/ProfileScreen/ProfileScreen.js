import { View, Text, ScrollView, StyleSheet } from 'react-native'
import React from 'react'
import Header from '../../components/Header';
import Footer from '../../components/Footer';
import { useNavigation } from '@react-navigation/native';

const ProfileScreen = () => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);

  const onPressHome = () => {
    navigation.navigate("MainScreen")
  }

  const onPressProfile = () => {
    scrollRef.current?.scrollTo({
        y: 0,
        animated: true,
      });
  }

  const onPressShop = () => {
      navigation.navigate("ShopScreen")
  }

  const onPressEvent = () => {
      navigation.navigate("EventScreen")
  }

  const onPressGroup = () => {
      navigation.navigate("GroupScreen")
  }

  return (

    <View style={styles.container}>
      <Header/>
      <ScrollView style={styles.container} ref={scrollRef}>
        <Text> Profile Screen </Text>
      </ScrollView>

      <Footer
        style={styles.footer}
        profileC="#3B71F3"
        onPressHome={onPressHome}
        onPressProfile={onPressProfile}
        onPressShop={onPressShop}
        onPressEvent={onPressEvent}
        onPressGroup={onPressGroup}
      />

    </View>
  )
}

const styles = StyleSheet.create({
    container: {
        flex: 1
    },
})

export default ProfileScreen