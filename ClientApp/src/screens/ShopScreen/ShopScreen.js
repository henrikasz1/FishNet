import { View, Text, ScrollView, StyleSheet } from 'react-native'
import React from 'react'
import Header from '../../components/Header';
import Footer from '../../components/Footer';
import { useNavigation } from '@react-navigation/native';

const ShopScreen = () => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);

  const onPressHome = () => {
    navigation.navigate("MainScreen")
  }

  const onPressProfile = () => {
      navigation.navigate("ProfileScreen")
  }

  const onPressShop = () => {
    scrollRef.current?.scrollTo({
        y: 0,
        animated: true,
      });
  }

  const onPressEvent = () => {
      navigation.navigate("EventScreen")
  }

  const onPressGroup = () => {
      navigation.navigate("GroupScreen")
  }

  const onPressSearch = () => {
    navigation.navigate("SearchScreen", {backScreen: "ShopScreen"})
  }

  return (

    <View style={styles.container}>
      <Header
        first={onPressSearch}
        second={onPressProfile}
      />
      <ScrollView style={styles.container} ref={scrollRef}>
        <Text> Shop Screen </Text>
      </ScrollView>

      <Footer
        style={styles.footer}
        shopC="#3B71F3"
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

export default ShopScreen