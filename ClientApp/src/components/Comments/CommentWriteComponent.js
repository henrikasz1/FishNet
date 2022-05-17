import React, { useState } from "react";
import { View, Text, Image, StyleSheet, TouchableOpacity, Button, TextInput, Keyboard } from "react-native";
import Icon from 'react-native-vector-icons/dist/FontAwesome';
import { BaseUrl } from "../../components/Common/BaseUrl";
import axios from "axios";
import FormData from 'form-data';
import CustomInput from '../../components/CustomInput';
import CustomButton from '../../components/CustomButton';

/* <View style={styles.icon}>
<Icon name="heart" size={17} color={haveThisCommentLiked ? "crimson" : "black"} />
</View> */

const CommentButton = ({onPress}) => {
  return (
    <TouchableOpacity onPress={onPress} style={styles.commentBtn}>
      <View style={styles.icon}>
        <Icon name="comment" size={24} color={'white'} />
      </View>
    </TouchableOpacity>
  )
}

export default function CommentWriteComponent ({ postId, commentWriterId, reloadFunction }) {

  const [comment, setComment] = useState('');
  const handleSubmit = async () => {
    const url = `${BaseUrl}/api/comments/${postId}`;
    await axios.post(url, {
      body: comment
    });
    await reloadFunction();
    Keyboard.dismiss();
    setComment('');
  }

  const onChange = (value) => {
    setComment(value);
  }

  return (
    <View style={{...styles.row, ...styles.br25}}>
      <View style={styles.firstBlock}>
        <TextInput
          multiline
          style={styles.input}
          placeholder="Body"
          onChangeText={onChange}
          value={comment}
        />
      </View>
      <View style={styles.secondBlock} >
        <CommentButton onPress={handleSubmit} />
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  br25: {
    borderColor: 'grey',
    borderWidth: 1,
    borderRadius: 25,
  },
  container: {
    flex: 1
  },
  row: {
    width: '100%',
    height: 40,
    flexDirection: 'row',
    alignItems: 'flex-end',
  },
  firstBlock: {
    width: '80%',
    flex: 1,
    paddingLeft: 10
  },
  secondBlock: {
    width: 40
  },
  icon: {
    borderRadius: 99,
    height: 40,
    width: 40,
    alignItems: 'center',
    justifyContent: 'center'
  },
  input: {
    borderColor: '#E8E8E8',
    borderWidth: 1,
    borderRadius: 25,
  },
  commentBtn: {
    backgroundColor: '#2196F3',
    borderRadius: 100
  }
});