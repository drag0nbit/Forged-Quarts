using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class SunVoxPlayer : MonoBehaviour {

  /*
     You can use SunVox library freely, 
     but the following text should be included in your products (e.g. in About window):

     SunVox modular synthesizer
     Copyright (c) 2008 - 2018, Alexander Zolotov <nightradio@gmail.com>, WarmPlace.ru

     Ogg Vorbis 'Tremor' integer playback codec
     Copyright (c) 2002, Xiph.org Foundation
  */

  //
  // * Loading SunVox song from file.
  // * Playing SunVox song.
  // * Stop/Play song.
  //

  public int volume = 256;
  public string path;
  public bool play = true;

  private void Start () {
    log ("-Press Space for toggle music-\n");

    try {
      int ver = SunVox.sv_init ("0", 44100, 2, 0);
      if (ver >= 0) {
        int major = (ver >> 16) & 255;
        int minor1 = (ver >> 8) & 255;
        int minor2 = (ver) & 255;
        log (String.Format ("SunVox lib version: {0}.{1}.{2}", major, minor1, minor2));

        SunVox.sv_open_slot (0);

        log ("Loading SunVox song from file...");
        if (SunVox.sv_load (0, path) == 0) {
          log ("Loaded.");
        } else {
          log ("Load error.");
          SunVox.sv_volume (0, volume);
        }

        SunVox.sv_play_from_beginning (0);

      } else {
        log ("sv_init() error " + ver);
      }

    } catch (Exception e) {
      log ("Exception: " + e);
    }
  }

  private void log (string msg) {
    Debug.Log (msg);
  }

private void Update () {
    if (play) SunVox.sv_play (0);
    else SunVox.sv_stop (0);
    SunVox.sv_volume (0, volume);
}

  private void OnDestroy () {
    if (!enabled) return;

    SunVox.sv_close_slot (0);
    SunVox.sv_deinit ();
  }

}