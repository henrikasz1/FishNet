import React, { useState, useEffect } from "react";
import { View, Text, StyleSheet, Animated, TouchableWithoutFeedback } from "react-native";
import ReadMore from 'react-native-read-more-text';

export default function CaptionComponent({ contents }){
  const [expanded, setExpanded] = useState(false);

  const toggleExpansion = () => {
    setExpanded(!expanded);
  };

  return (
    <View>
      <ReadMore numberOfLines={3}>
        <Text style={styles.text}>
          {contents}
        </Text>
      </ReadMore>
    </View>
  );
}

const styles = StyleSheet.create({
  text: {
    color: '#353839'
  }
});
