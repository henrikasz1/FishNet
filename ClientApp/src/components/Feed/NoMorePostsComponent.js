import React from 'react';
import { View, Text, Image } from 'react-native';
const fishImage = require('../../../assets/images/plentymorefish.png');

export default function NoMorePostsComponent() {
  const style = {
    color: 'gray',
    fontSize: 14,
  };

  return (
    <View>
      <Text style={style}> You've viewed all your friend's posts </Text>
      <Text> Catch some fish tommorrow! </Text>
      <Image source={fishImage} />
    </View>
  );
}
