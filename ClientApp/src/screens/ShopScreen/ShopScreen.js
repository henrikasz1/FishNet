import { View, Text, ScrollView, StyleSheet } from 'react-native'
import React, {useState} from 'react'
import Header from '../../components/Header';
import ShopSearchHeader from '../../components/SearchHeader';
import Footer from '../../components/Footer';
import { useNavigation } from '@react-navigation/native';
import { BaseUrl } from '../../components/Common/BaseUrl';
import axios from 'axios';
import Product from '../../components/Product';
import MarketplaceHeader from '../../components/MarketplaceHeader';

const ShopScreen = () => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);
  const [data, setData] = useState([])
  const [loading, setLoading] = useState(true)
  const [search, setSearch] = useState('')
  const searchUrl = `${BaseUrl}/api/shop/shopbyname/${search}`;

  const handleLoad = async() => {
    const url = `${BaseUrl}/api/shop/allshopads`
    const shopItems = (await axios.get(url)).data
    setData(shopItems)
    setLoading(false)
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

  const onPressProduct = (shopItem) => {
    const { description, title, price, location, photos, userId} = shopItem;
    navigation.navigate("PostAdvertsScreen",
     {backScreen: "ShopScreen", description, title, price, location, photos, userId})
  }

  const onPressSearch = () => {
    navigation.navigate("ShopSearchScreen")
  }

  if (loading) {
    handleLoad()
  }

  

  return (

    <View style={styles.container}>
      <MarketplaceHeader first={onPressSearch}>
      </MarketplaceHeader>
      <ScrollView ref={scrollRef}>
        <View style={{...styles.container, ...styles.shop}}>{data.map((shopItem, key) => { 
          return <Product
            onPressProduct={() => onPressProduct(shopItem)}
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