import { View, Text, ScrollView, StyleSheet } from 'react-native'
import React, {useState, useEffect} from 'react'
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
  const getCurrentUserId = `${BaseUrl}/api/user/getuserid`

  const [data, setData] = useState([])
  const [loading, setLoading] = useState(true)
  const [currentUserId, setCurrentUserId] = useState("")

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
    navigation.push("ProfileScreen", {userId: currentUserId});
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
      <ScrollView ref={scrollRef}>
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