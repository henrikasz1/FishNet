import React from 'react';
import { View, Text, Image } from 'react-native';
const fishImage = require('../../../assets/images/plentymorefish.png');
const Block = require('./FeedBlock');

export default function NoMorePostsComponent() {

  return (
    <View>
      {/* <Block
        title="You've viewed all your friend's posts"
        photo={fishImage}
        caption="Catch some fish tommorrow!"
        onClick={() => {}} 
      />*/}
    </View>
  );
}
