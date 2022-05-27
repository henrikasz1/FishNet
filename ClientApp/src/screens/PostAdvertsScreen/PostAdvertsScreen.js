import { View, Text, ScrollView, StyleSheet } from 'react-native'
import React, {useState} from 'react'
import Header from '../../components/Header';
import SearchHeader from '../../components/SearchHeader';
import Footer from '../../components/Footer';
import { useNavigation } from '@react-navigation/native';
import { BaseUrl } from '../../components/Common/BaseUrl';
import axios from 'axios';
import Product from '../../components/Product';
import CarouselComponent from '../../components/Feed/CarouselComponent';

const ShopScreen = ({route}) => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);
  const {description, title, price, location, photos, shopId} = route.params;
  const [data, setData] = useState([])
  const [loading, setLoading] = useState(true)

  const handleLoad = async() => {
    const url = `${BaseUrl}/api/shop/allshopads`
    const shopItems = (await axios.get(url)).data
    setData(shopItems)
    setLoading(false)
    console.log(shopItems)
  }

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

  if (loading) {
    handleLoad()
  }

  return (

    <View style={styles.container}>
      <Header
        first={onPressSearch}
        second={onPressProfile}
      />
    
    <View style={{flex: 1}}>
        <Text>{description}</Text>
        <Text>{location}</Text>
        <Text>{title}</Text>
        <Text>{price}</Text>
        <CarouselComponent photos={photos}></CarouselComponent>
    </View>

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
    shop: {
      flexDirection: 'row',
      flexWrap: 'wrap',
    }
})

export default ShopScreen