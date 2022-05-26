import { View, Text, ScrollView, StyleSheet } from 'react-native'
import React, {useState, useEffect} from 'react'
import Header from '../../components/Header';
import Footer from '../../components/Footer';
import { useNavigation } from '@react-navigation/native';
import { BaseUrl } from '../../components/Common/BaseUrl';
import axios from 'axios';

const GroupScreen = () => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);
  const getCurrentUserId = `${BaseUrl}/api/user/getuserid`

  const [currentUserId, setCurrentUserId] = useState("")

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
      navigation.push("ProfileScreen", {currentBackScreen: "GroupScreen", userId: currentUserId});
  }

  const onPressEvent = () => {
      navigation.navigate("EventScreen")
  }

  const onPressSearch = () => {
    navigation.navigate("SearchScreen", {backScreen: "GroupScreen"})
  }

  useEffect(() => {
    axios
      .get(getCurrentUserId)
      .then(response => {
        setCurrentUserId(response.data);
      })
      .catch(err => {
        console.log(err)
      })

    }, [])

  return (

    <View style={styles.container}>
      <Header
        first={onPressSearch}
        second={onPressProfile}
      />
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