import { View, Text, StyleSheet, Button, Image, TextInput, ScrollView, ActivityIndicator } from 'react-native'
import React, {useState} from 'react'
import * as ImagePicker from 'react-native-image-picker'
import DefaultUserPhoto from '../../../assets/images/default-user-image.png'
import CustomInput from '../../components/CustomInput';
import CustomButton from '../../components/CustomButton';
import FormData from 'form-data'
import { useNavigation } from '@react-navigation/native';
import { BaseUrl } from '../../components/Common/BaseUrl'
import axios from 'axios';
import PhotoManagerComponent from '../../components/CreatePost/PhotoManagerComponent';
import MultipleImagePicker from '@baronha/react-native-multiple-image-picker';


const CreatePostScreen = () => {

  const [images, setImages] = useState([]);
//   const [type, setType] = useState('');
  const [body, setBody] = useState('');
//   const [name, setName] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const navigation = useNavigation();

  const handleSkip = () => {
    navigation.navigate('MainScreen');
  }

  const handleChoosePhoto = async () => {
    const options = {
      noData: true,
    };

    const response = await MultipleImagePicker.openPicker(options);

    const _photos = response.map(entry => {
      return {
        name: entry.fileName,
        type: entry.mime,
        uri: entry.path
      };
    })


    console.log(response);
    setImages(_photos);
    setIsSubmitting(false);
  };

  const handleSavePhoto = () => {
    console.log("UPLOAD");
    const url = `${BaseUrl}/api/post`;

    setIsSubmitting(true);

    var data = new FormData();

    images.forEach(image => {
        data.append('files', {
          uri: Platform.OS === 'android' ? image.uri : image.uri.replace('file://', ''),
          name: image.name || 'photo.jpg',
          type: image.type
        });
    })


    data.append('body', body);
    
    const config = { headers: {'Content-Type': 'multipart/form-data;' } };

    axios
        .post(url, data, config)
        .then((response) => {
          navigation.navigate('MainScreen');
          setIsSubmitting(false);
        })
        .catch(error => {
          setIsSubmitting(false);
        })
    navigation.navigate('MainScreen');
  };

  return (
    <ScrollView showsVerticalScrollIndicator={false}>
      <View style={styles.root}>

        <View style={styles.header}>
            <Text style={styles.title}> Create a post </Text>
            <Text style={styles.skip} onPress={handleSkip}>CANCEL</Text>
        </View>

        <PhotoManagerComponent pics={images}/>
        <TextInput
            multiline
            style={[styles.input, {backgroundColor: 'white'}]}
            placeholder="Body"
            onChangeText={setBody}
         />

        <CustomButton 
          style={styles.button}
          text="Upload Photos"
          onPress={handleChoosePhoto}
        />

        {!isSubmitting ?
          <CustomButton 
            style={styles.button}
            text="Save"
            onPress={handleSavePhoto}
          />
          :
          <CustomButton 
            text={<ActivityIndicator size="small" color="white" />}>
          </CustomButton>
        }

      </View>
    </ScrollView>
  )
}

const styles = StyleSheet.create({
  header: {
    flex: 1,
    flexDirection: 'row',
    width: '100%',
    textAlign: "center"
  },
  skip: {
    marginTop: "1.5%",
    fontSize: 15,
    left: "-80%"
  },
  root: {
      alignItems: 'center',
      padding: 20,
  },
  title: {
    width: "100%",
    fontSize: 24,
    fontWeight: 'bold',
    color: '#051C60',
    textAlign: "center",
    marginBottom: '8%'
  },
  image: {
    borderRadius: 20,
    width: 300,
    height: 300,
    marginBottom: '7%'
  },
  input: {
    backgroundColor: '#ededed',
    borderRadius: 5,
    borderColor: '#e0e0e0',
    borderWidth: 1,
    paddingHorizontal: 20,
    width: '100%',
    marginBottom: '8%'
  }
});

export default CreatePostScreen;
