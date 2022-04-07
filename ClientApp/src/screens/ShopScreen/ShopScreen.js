import { View, Text, ScrollView, StyleSheet } from 'react-native'
import React, {useState} from 'react'
import Header from '../../components/Header';
import SearchHeader from '../../components/SearchHeader';
import Footer from '../../components/Footer';
import { useNavigation } from '@react-navigation/native';
import { BaseUrl } from '../../components/Common/BaseUrl';
import axios from 'axios';
import Product from '../../components/Product';

const ShopScreen = () => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);

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
      <ScrollView ref={scrollRef}>
        <Text>Filters</Text> {/*todo filters*/}
        <View style={{...styles.container, ...styles.shop}}>{data.map((shopItem, key) => { 
          console.log(shopItem);
          return <Product
            description={shopItem.description}
            title={shopItem.productName}
            photos={shopItem.photos}
            price={shopItem.price}
            location={shopItem.location}
            shopId={shopItem.shopId}
            key={key}
          />
        })}</View>
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
    shop: {
      flexDirection: 'row',
      flexWrap: 'wrap',
    }
})

export default ShopScreen