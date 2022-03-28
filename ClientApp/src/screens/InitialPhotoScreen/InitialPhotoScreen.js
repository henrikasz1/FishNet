import { View, Text, StyleSheet, Button, Image, TextInput, ScrollView, ActivityIndicator } from 'react-native'
import React, {useState} from 'react'
import * as ImagePicker from 'react-native-image-picker'
import DefaultUserPhoto from '../../../assets/images/default-user-image.png'
import CustomInput from '../../components/CustomInput';
import CustomButton from '../../components/CustomButton';
import FormData from 'form-data'
import { useNavigation } from '@react-navigation/native';
import axios from 'axios';


const InitialPhotoScreen = () => {

  const [photo, setPhoto] = useState(null);
  const [type, setType] = useState('');
  const [body, setBody] = useState('');
  const [name, setName] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const navigation = useNavigation();

  const handleSkip = () => {
    navigation.navigate('MainScreen');
  }

  const handleChoosePhoto = () => {
    const options = {
      noData: true,
    };

    ImagePicker.launchImageLibrary(options, response => {
      if (!response.didCancel)
      {
        setName(response.assets[0].fileName);
        setType(response.assets[0].type);
        setPhoto(response.assets[0].uri);
      }
    })

    setIsSubmitting(false);
  }

  const handleSavePhoto = () => {
    const url = "http://10.0.2.2:5000/api/userphoto/upload";

    setIsSubmitting(true);

    var data = new FormData();

    data.append('file', {
      uri: Platform.OS === 'android' ? photo : photo.replace('file://', ''),
      name: name || 'photo.jpg',
      type: type
    });

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
  };

  return (
    <ScrollView showsVerticalScrollIndicator={false}>
      <View style={styles.root}>

        <View style={styles.header}>
            <Text style={styles.title}> Add your first photo </Text>
            <Text style={styles.skip} onPress={handleSkip}>SKIP</Text>
        </View>

        {photo !== null ?
          <Image
            source={{ uri: photo }}
            style={styles.image}
          />
          :
          <Image
            source={DefaultUserPhoto}
            style={styles.image}
          />
        }

        {photo !== null ?
          <TextInput
            multiline
            style={[styles.input, {backgroundColor: 'white'}]}
            placeholder="Body"
            onChangeText={setBody}
         />
         :
          <TextInput
            editable={false}
            multiline
            style={styles.input}
            placeholder="Body"
          />
        }

        <CustomButton 
          style={styles.button}
          text="Choose Photo"
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
    left: "-40%"
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
    borderRadius: 150,
    width: 300,
    height: 300,
    marginBottom: '7%'
  },
  input: {
    backgroundColor: '#ededed',
    borderRadius: 5,
    borderColor: '#e0e0e0',
    borderWidth: 1,
    borderRadius: 5,
    paddingHorizontal: 20,
    width: '100%',
    marginBottom: '8%'
  }
});

export default InitialPhotoScreen