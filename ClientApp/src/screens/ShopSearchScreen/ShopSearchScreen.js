import { View, Text, ScrollView, useWindowDimensions, ActivityIndicator, StyleSheet, Image } from 'react-native'
import { useNavigation, CommonActions } from '@react-navigation/native';
import SearchHeader from '../../components/SearchHeader';
import SearchResult from '../../components/SearchResult';
import axios from 'axios';
import React, { useState, useEffect } from 'react';
import DeadFish from '../../../assets/images/deadFish.png';
import Fish from '../../../assets/images/Fish.png';
import { BaseUrl } from '../../components/Common/BaseUrl';

const ShopSearchScreen = ({route}) => {

  const [search, setSearch] = useState('')
  const [searching, setSearching] = useState(false)
  const [results, setResults] = useState([])
  const [text, setText] = useState()
  const [photo, setPhoto] = useState()

  const navigation = useNavigation();

  const {height} = useWindowDimensions();
  const searchUrl = `${BaseUrl}/shop/shopbyname/${search}`;

  useEffect(() => {
    var res = []
    const getResults = async () => {
      await setSearching(true)

      await axios
        .get(searchUrl)
        .then(response => {
          setResults(response.data)
        })
        .catch(err => {
          console.log(err)
        })

      await setSearching(false)
    }

    if (search === '')
    {
      setText('Fish for new products!')
      setPhoto(Fish)
      setResults([])
    }
    else
    {
      getResults()
      setText(`Uh oh, we couldn't find anything!`)
      setPhoto(DeadFish)
    }
    console.warn(results)
  }, [search])

  return (
    
    <View style={styles.root}>
      <SearchHeader
        onPressBack={() => navigation.navigate("ShopScreen")}
        value={search}
        setValue={setSearch}
      />

      { results.length > 0 ? 
        <ScrollView>
          {results.map((result, index) => (
            <SearchResult
              key={index}
              id={result.enti}
              name={entityName}
              photo={entityMainPhotoUrl}
            />
          ))}
        </ScrollView>
        :
        <View>
          {searching ?
            <View style={styles.container}>
                <ActivityIndicator size="large" color="#3B71F3" />
            </View>
            :
            <View style={styles.container}>
                <Image source={photo} style={{height: height * 0.25}} resizeMode="contain" />
                <Text> {text} </Text>
            </View >
          }
        </View>
      }
    </View>
  )
}

const styles = StyleSheet.create({
    root: {
        height: '100%',
        flex: 1,
    },
    container: {
        height: '90%',
        justifyContent: 'center',
        alignItems: 'center'
    },
})

export default ShopSearchScreen