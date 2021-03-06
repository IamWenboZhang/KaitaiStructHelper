﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaitai
{
    public partial class TcpSegment : KaitaiStruct
    {
        public static TcpSegment FromFile(string fileName)
        {
            return new TcpSegment(new KaitaiStream(fileName));
        }

        public TcpSegment(KaitaiStream p__io, KaitaiStruct p__parent = null, TcpSegment p__root = null) : base(p__io)
        {
            m_parent = p__parent;
            m_root = p__root ?? this;
            _read();
        }
        private void _read()
        {
            _srcPort = m_io.ReadU2be();
            _dstPort = m_io.ReadU2be();
            _seqNum = m_io.ReadU4be();
            _ackNum = m_io.ReadU4be();
            _b12 = m_io.ReadU1();
            _b13 = m_io.ReadU1();
            _windowSize = m_io.ReadU2be();
            _checksum = m_io.ReadU2be();
            _urgentPointer = m_io.ReadU2be();
            _body = m_io.ReadBytesFull();
        }
        private ushort _srcPort;
        private ushort _dstPort;
        private uint _seqNum;
        private uint _ackNum;
        private byte _b12;
        private byte _b13;
        private ushort _windowSize;
        private ushort _checksum;
        private ushort _urgentPointer;
        private byte[] _body;
        private TcpSegment m_root;
        private KaitaiStruct m_parent;
        public ushort SrcPort { get { return _srcPort; } }
        public ushort DstPort { get { return _dstPort; } }
        public uint SeqNum { get { return _seqNum; } }
        public uint AckNum { get { return _ackNum; } }
        public byte B12 { get { return _b12; } }
        public byte B13 { get { return _b13; } }
        public ushort WindowSize { get { return _windowSize; } }
        public ushort Checksum { get { return _checksum; } }
        public ushort UrgentPointer { get { return _urgentPointer; } }
        public byte[] Body { get { return _body; } }
        public TcpSegment M_Root { get { return m_root; } }
        public KaitaiStruct M_Parent { get { return m_parent; } }
    }
}
