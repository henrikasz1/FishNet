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
import Icon from 'react-native-vector-icons/dist/FontAwesome';

const ShopScreen = ({route}) => {
  
  const navigation = useNavigation();
  const scrollRef = React.useRef(null);
  const {description, title, price, location, photos, userId} = route.params;
  const [data, setData] = useState([])
  const [loading, setLoading] = useState(true)

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

  const onPressSearch = () => {
    navigation.navigate("SearchScreen", {backScreen: "ShopScreen"})
  }

  const onPressBack = () => {
    navigation.navigate("ShopScreen")
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
      <View>
        <Icon name={"chevron-left"} size={25} color={"#353839"} onPress={onPressBack}/>
      </View>
    <View style={{flex: 1, height: 500, width: 300}}>
        <CarouselComponent pics={photos} style={styles.itemPhotos}></CarouselComponent>
        <Text style={styles.nameText}>{userId}</Text>
        <Text style={styles.text}>{description}</Text>
        <Text style={styles.text}>{location}</Text>
        <Text style={styles.text}>{title}</Text>
        <Text style={styles.text}>{price}</Text>
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
    },
    itemPhotos: {
      alignItems: 'center',
      flex: 1,
      justifyContent: 'center'
    },
    text: {
      fontWeight: 'bold',
      fontSize: 17
    },
    nameText: {
      fontWeight: 'bold',
      fontSize: 20
    }
})

export default ShopScreen