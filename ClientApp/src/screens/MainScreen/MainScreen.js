import { View, Text, Appearance, ScrollView, StyleSheet, ActivityIndicator, Alert } from 'react-native'
import Header from '../../components/Header';
import Footer from '../../components/Footer';
import React, { useState, useEffect } from 'react';
import axios from 'axios';
import Block from '../../components/Feed/FeedBlock';
import { BaseUrl } from '../../components/Common/BaseUrl';
import fishImage from '../../../assets/images/plentymorefish.png';
import InterjectComponent from '../../components/Feed/InterjectComponent';
import { useNavigation } from '@react-navigation/native';

//BUG
//dubliuojasi
//todo fix
const mergeWithUID = (source, obj) => {
  const sourcePostIds = source.map(m => m.postId);
  let dest = [...source];
  obj.filter(post => !sourcePostIds.includes(post.postId))
    .forEach(newPost => dest.push(newPost));
  return dest;
}

const MainScreen = () => {

  const [data, setData] = useState([]);
  const [loading, setLoaded] = useState(true);
  const [error, setError] = useState(null);
  const [batchNumber, setBatchNum] = useState(0);
  const [isPublicPosts, setFetchingPublic] = useState(false);
  const [likerId, setUserId] = useState('');
  const [isWarm, setIsWarm] = useState(false);

  const scrollRef = React.useRef(null);
  const navigation = useNavigation();
  const userIdUrl = `${BaseUrl}/api/user/getuserid`;

  const onPressHome = () => {
    scrollRef.current?.scrollTo({
      y: 0,
      animated: true,
    });
  }

  const onPressProfile = () => {
    navigation.navigate("ProfileScreen")
  }

  const onPressShop = () => {
    navigation.navigate("ShopScreen")
  }

  const onPressEvent = () => {``
    navigation.navigate("EventScreen")
  }

  const onPressGroup = () => {
    navigation.navigate("GroupScreen")
  }

  const onPressSearch = () => {
    navigation.navigate("SearchScreen", {backScreen: "MainScreen"})
  }

  const feedWarmUp = async () => {
    
    const url = `${BaseUrl}/api/post/allfriendposts?batchsize=${batchNumber}`;

    await axios.get(url).then((response) => {
      if (response.status == '200') {
        if (response.data.length === 0) {
          setFetchingPublic(true);
        }
      }
    })

    await setIsWarm(true);
  }

  //FETCH REAL DATA FROM DOT NET
  const handleLoad = async () => {

    await axios.get(userIdUrl).then(res => setUserId(res.data));
    
    const url =  `${BaseUrl}/api/post/${isPublicPosts ? 'remainingposts' : 'allfriendposts'}?batchsize=${batchNumber}`;
    
    await axios.get(url).then((response) => {
      if (response.status == '200') {
        if (response.data.length === 0){
          setBatchNum(batchNumber - 1);
          setFetchingPublic(true);
        }
        response.data.forEach((post) => {
          if (!isPublicPosts){
            post.isFriendPost = true;
          }
        });
        setData(mergeWithUID(data, response.data));
      } else {
        throw new Error(response.status);
      }
    }).catch((e) => {
      setError(e);
    });
  }

  const handleRemovePostFromState = (postId) => {
    setData(data.filter(post => post.postId != postId));
  }

  const handleLoadMore = () => {
    setBatchNum(batchNumber + 1);
    handleLoad();
  }

  const bearer = axios.defaults.headers.common.Authorization;
  console.log(bearer);

  useEffect(() => {
      // this is for solving react state update on an umounted component problem
      let isMounted = true;

      if (isMounted)
      {
        const GetResult = async () => {

          if (isWarm)
          {
            if (loading)
            {
              await handleLoad();
              setLoaded(false)
            }
          }
          else
          {
            await feedWarmUp();
          }
        }
  
        GetResult();
      }
     
      return () => { isMounted = false };
  }, [isWarm, loading]);

  const isCloseToBottom = ({ layoutMeasurement, contentOffset, contentSize }) => {
    const paddingToBottom = 20;
    return layoutMeasurement.height + contentOffset.y >=
      contentSize.height - paddingToBottom;
  };

  return (
    <View style={styles.container}>
      <Header
        first={onPressSearch}
        second={onPressProfile}
      />

      { loading ? (
        <View style={styles.cc}>
          <ActivityIndicator size="large" />
        </View>
      ) : data ? (
        <ScrollView style={styles.container} onScroll={(event) => {
          if (isCloseToBottom(event.nativeEvent)) {
            handleLoadMore();
          }
        }}  ref={scrollRef}>
          {data.length ? data
            .filter(post => post.isFriendPost == true)
            .map(({ title, photos, postId, userId, likesCount, body, commentsCount }, index) => (
          <Block
            title={title}
            photo={photos}
            caption={body}
            key={index}
            postId={postId}
            userId={userId}
            likesCount={likesCount}
            likerId={likerId}
            commentsCount={commentsCount}
            onDelete={handleRemovePostFromState}
            isFriendPost={true}
            goBackComments="MainScreen"
          />
        )) : (
            <Block
              title="You've viewed all of your friends' posts"
              photo={fishImage}
              caption="Catch some fish tommorrow!"
            />
          )}
        {/* {isPublicPosts && <InterjectComponent text={"Public posts"} />} */}
        {data.length ? data
            .filter(post => !post.isFriendPost)
            .map(({ title, photos, postId, userId, likesCount, body, commentsCount }, index) => (
          <Block
            title={title}
            photo={photos}
            caption={body}
            key={index}
            postId={postId}
            userId={userId}
            likesCount={likesCount}
            likerId={likerId}
            commentsCount={commentsCount}
            onDelete={handleRemovePostFromState}
            isFriendPost={false}
            goBackComments="MainScreen"
          />
        )) : (
            <Block
              title="You've viewed all the public posts"
              photo={fishImage}
              caption="Catch some fish tommorrow!"
            />
          )}
      </ScrollView>
      ) : (
        <View style={styles.bottom}>
          {error && <Text>"Error" {error.message}</Text>}
        </View>
      )}
      <Footer
        style={styles.footer}
        homeC="#3B71F3"
        onPressProfile={onPressProfile}
        onPressHome={onPressHome}
        onPressShop={onPressShop}
        onPressEvent={onPressEvent}
        onPressGroup={onPressGroup}
      />
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#e0e0e0'
  },
  cc: {
    flex: 1,
    backgroundColor: '#e0e0e0',
    justifyContent: 'center'
  },
  bottom: {
    flex: 1,
    backgroundColor: '#e0e0e0',
    justifyContent: 'flex-end'
  }
});

export default MainScreen;