import React from 'react';
import { ScrollView, View } from 'react-native';

export default function LazyLoadParent() {
  const container = React.createRef(null);
  return (
    <>
      <ScrollView
        showsVerticalScrollIndicator={false}
        scrollEventThrottle={10}
        onScroll={event => {
          container.current.onScroll();
        }}
        bounces={false}
        scrollEnabled={true}>
        <View>
        </View>
      </ScrollView>
    </>
  );
}
