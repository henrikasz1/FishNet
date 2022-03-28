import React, { useState, useEffect } from "react";
import { View, Text, StyleSheet, Animated, TouchableWithoutFeedback } from "react-native";

export default function CaptionComponent({ contents }){
  const [expanded, setExpanded] = useState(false);

  const toggleExpansion = () => {
    setExpanded(!expanded);
  };

  return (
    <TouchableWithoutFeedback onPress={toggleExpansion}>
      <View>
        <Text numberOfLines={expanded ? 30 : 2} ellipsizeMode="tail">
          {contents}
        </Text>
        {!expanded && <Text style={styles.actionable}>...read more</Text>}
      </View>
    </TouchableWithoutFeedback>
  );
}

const styles = StyleSheet.create({
  container: {
    alignItems: 'center',
    justifyContent: 'center',
    padding: 24,
  },
  actionable: {
    color: 'darkgray'
  }
});
